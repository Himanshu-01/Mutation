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

   The Global library is some hacked up "glue" code that I created a long time ago to circumvent the circular dependencies issue in .Net. I am currently working towards removing this library entirely.

* HaloControls

   This library is a set of .Net user controls that are used to edit halo meta data elements in tag files. These are primarily used by the tag editor in the Mutation GUI. They are currently built using DevExpress libraries which I am working towards removing completely.

* HaloMap

   The HaloMap library is the core code for reading, decompiling, and rebuilding map files for halo 2. All map file related code is in this library.

* HaloObjects

   The HaloObjects library is a set of custom data types that are used in the halo tag system. They are used in the tag layouts and in the halo map code. They are also used in some of the GUI code.

* HaloPlugins

   The HaloPlugins library has all of the tag layouts for the various tag files used in the map files for halo 2. There are also some tag block layouts that have been isolated into their own files. This is due to those tag block layouts being used in multiple other layouts in the engine.

* IO

   This library is an extension of the stock BinaryReader/BinaryWriter and provides the ability to write to files in both little endian and big endian.

* LayoutViewer

   LayoutViewer is an application that can be used to view all of the tag layouts directly from the halo 2 PC Guerilla.exe editor. I developed this to make an easy way to view the tag layouts, compare them to the ones I have created for Mutation, and export them into a format that Mutation can use. It requires two files from the Halo 2 PC HEK in order to run: Guerilla.exe and h2alang.dll. Copy both of these files into the same folder as the LayoutViewer.exe and it will load all of the tag layouts from the Halo 2 PC toolkit.

* Libraries

   This folder contains various 3rd party dll files that are referenced throughout the project for GUI elements and such.

* Mutation.HEK

   This library contains code that is used to read out information from the Halo 2 PC edition kit. This is the core code for reading the tag layouts out of the Guerilla.exe executable.

* Mutation.Halo

   This library will eventually replace HaloMap, HaloObjects, and HaloPlugins and become the new core library for all things halo map file, tag file, and assset file related.

* Mutation

   The main Mutation application and UI.

* Portal

   The Portal library is a bunch of deprecated code for rendering various assets in Halo 2 using DirectX. Most of this code relies on deprecated tag layout files so it has been removed from the main project solution.

* Xapien

   The Xapien library contains a bunch of DirectX GUI controls. It will eventually be used for in-rendering editors.
