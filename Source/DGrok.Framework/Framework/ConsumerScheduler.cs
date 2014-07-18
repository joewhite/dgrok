// Copyright (c) 2007-2014 Joe White
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;

namespace DGrok.Framework
{
    public class ConsumerScheduler<T>
    {
        private Action<T> _action;
        private AutoResetEvent _allDoneEvent;
        private BackgroundWorker _backgroundWorker;
        private object _mutex = new object();
        private Queue<T> _queue;
        private int _runningThreadCount;
        private string _taskDescription;
        private int _threadCount;

        public ConsumerScheduler(BackgroundWorker backgroundWorker, IEnumerable<T> items, Action<T> action,
            int threadCount, string taskDescription)
        {
            if (threadCount < 1)
                throw new ArgumentOutOfRangeException("threadCount", "Thread count must be at least 1");
            _backgroundWorker = backgroundWorker;
            _queue = new Queue<T>(items);
            _action = action;
            _threadCount = threadCount;
            _taskDescription = taskDescription;
        }

        private void CheckForCancel()
        {
            if (_backgroundWorker.CancellationPending)
                throw new CancelException();
        }
        public void Execute()
        {
            using (_allDoneEvent = new AutoResetEvent(false))
            {
                StartThreads();
                WaitForThreadsToFinish();
            }
        }
        private void StartThreads()
        {
            List<Thread> threads = new List<Thread>();
            _runningThreadCount = _threadCount;
            for (int i = 0; i < _threadCount; ++i)
                ThreadPool.QueueUserWorkItem(ThreadProc);
        }
        private void ThreadProc(object state)
        {
            try
            {
                while (true)
                {
                    CheckForCancel();
                    T item;
                    lock (_mutex)
                    {
                        if (_queue.Count == 0)
                            return;
                        item = _queue.Dequeue();
                    }
                    _action(item);
                }
            }
            catch (CancelException)
            {
            }
            finally
            {
                lock (_mutex)
                {
                    --_runningThreadCount;
                    if (_runningThreadCount <= 0)
                        _allDoneEvent.Set();
                }
            }
        }
        private void WaitForThreadsToFinish()
        {
            while (!_allDoneEvent.WaitOne(250, false))
            {
                int itemsRemaining;
                lock (_mutex)
                    itemsRemaining = _queue.Count;
                string progressMessage = _taskDescription + " (" + itemsRemaining + " item(s) remaining)";
                _backgroundWorker.ReportProgress(0, progressMessage);
            }
            CheckForCancel();
        }
    }
}
