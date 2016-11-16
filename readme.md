# Rust interop

Just a place to practice interop between Rust and (currently) F#.

### Simple functions

There is nothing fancy here, just calling simple functions in Rust, such as "Add."


### Building

The F# project contains a link reference to the DLL outputted by building the Rust code in release.
There isn't a fancy build system, just depending on Cargo to build Rust and VS2015 to build F#.