# 0x7 Unpacker
0x7 Unpacker is an open-source deobfuscator for 0x7 Obfuscator (Old Version of Inx Obfuscator, mainly pasted from ConfuserEx). License: MIT.

This is my first time making an unpacker, excuse the messy code.

## Usage
Compile
Drag and Drop Packed assembly onto Compiled Unpacker exe

## Fully Supported Protections
 * Anti Dump
 * Strings Encoding (Compress / encode strings) XOR can be decrypted with de4dot, therefore it was not added.
 * Int Math

## Semi - Supported Protections
 * AntiVM - Only works without CFLOW

## Todo / Currently Adding 
 * Constant Mutation - Nearly done
 * Constants Outliner
 * Reference Proxy - Code Finished just implementing
 * Control Flow - Low is supported but not included. Max will be the next done, nothing in-between for now.
 * Local To Field

## Special thanks
 * [Kaidoz](https://github.com/Kaidoz) - Proxy Methods, Constants Outliner
 * [0xd4d](https://github.com/0xd4d) - [dnlib](https://github.com/0xd4d/dnlib)
 * [TheProxy](https://github.com/TheProxyRE) - [String Tutorial](https://github.com/TheProxyRE/Deobfuscation-Tutorials/tree/master/1.Strings)
 * [TheProxy](https://github.com/TheProxyRE) - Mutations, provided I had to unpack your cleaner because you used VMP which was WHY!?!
