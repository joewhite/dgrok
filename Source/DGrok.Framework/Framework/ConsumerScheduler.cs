// Copyright 2007, 2008 Joe White
//
// This file is part of DGrok <http://www.excastle.com/dgrok/>.
//
// DGrok is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// DGrok is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with DGrok.  If not, see <http://www.gnu.org/licenses/>.
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
