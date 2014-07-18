DGrok Delphi parser
===================

DGrok is all about parsing Delphi source code. It has three parts:

* A Delphi grammar: The "DGrok Delphi grammar" documents the grammar for the Delphi language.
* A parser: The "DGrok Delphi parser" is an open-source Delphi parser written in C#. It reads Delphi source files and turns them into parse trees.
* A set of tools: The "DGrok tools" are a set of open-source tools built on top of the parser.

The current parser has full support for Delphi 2007 source code (to the best of my knowledge), but
no support for Delphi 2009 features like generics. There's also no symbol table support, so the
tools can't do refactorings or Find References.

More information is available on the [DGrok website](http://dgrok.excastle.com/).

What's included
---------------

**Grammar.html** documents the Delphi grammar.

Everything else is in the Source directory. **DGrok.Framework.dll** is the main parser.
**DGrok.Demo.exe** provides a GUI you can use to parse snippets of code or directory trees on disk,
and run some predefined analyses on the resulting parse tree.

Compiling DGrok
---------------

You should be able to compile the included code as-is, using Visual Studio 2005 (or later) or
MSBuild.

If you want to actually make changes to the code, you'll want to install
[Ruby](https://www.ruby-lang.org/) and Rake (`gem install rake`), and then open a command prompt
in the Source directory and type:

    rake

This will rebuild the Grammar.html file in the Source directory, do all the nifty codegen stuff,
build everything, and run the tests. There are other Rake targets to do other things; see the
Rakefile for details.

Beyond that, you're kind of on your own. I (Joe White, the original author) no longer use Delphi,
so I'm no longer actively maintaining DGrok. I'll be happy to accept pull requests, though.

Happy parsing!