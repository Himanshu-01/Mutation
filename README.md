# Mutation
Halo 2 content creation tool

Mutation is a tool that aims to be an all in one content creation tool and level editor for halo 2 for the xbox platform. Currently it is able to decompile existing map files and recompile most of them, although some build issues exist.

## Projects
The entire project is currently built in C# .Net and I hope to leave it this way, and possibly look to porting it to MacOS in the future. Below is a description of each project and what it is used for. For a more complete description of the projects including dependencies and usage info, see the Readme.md file in the respective project folder.

* Blam

   Blam is a library that contains code I have reverse engineered from the Halo 2 PC HEK (Halo Editing Kit, ie Guerilla, Sapien, or Tool). While I was creating Mutation I decided to investigate how some of the underlying systems work in the real Bungie tools. This is just a dumping ground for my findings. It serves so real purpose for the project.

* DDS

   DDS is a image library that is capable of encoding and decoding the various DXT image formats that are used in the game. Most of the encoding/decoding code was written by Jackson Cougar, I have just encapsulated it into a class for easy access throughout the project. It currently supports the following formats:
   * A8
   * Y8
   * AY8
   * A8Y8
   * R5G6B5
   * A1R5G5B5
   * A4R4G4B4
   * X8R8G8B8
   * A8R8G8B8
   * DXT1
   * DXT3
   * DXT5
   * P8
   * Lightmap
   * U8V8

* Global

   The Global
