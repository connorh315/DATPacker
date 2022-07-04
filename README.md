# DATPacker

Builds TTGames archives from a folder.

# PLEASE READ

This tool currently has the following limitations:

- Can only create first year, LEGO Dimensions DAT files (NOT DLC, NOT PATCH FILES - BASE GAME FILES ONLY!)

# How to use

Ensure you have .NET 6.0 installed, and download the latest release of DATPacker from GitHub.

Open the program and select the folder that you wish to build an archive from.

Select the output file location and click build.

This sounds easy, but there are many caveats:

- Do not place the same file in multiple archives, i.e. do not place STUFF\TEXT.CSV in both GAME.DAT and GAME0.DAT. This means that if you want to update a file already in an archive, you will need to unpack that archive (using either QuickBMS or DATManager) and rebuild and replace that archive with your updated build.
- If a PATCH.DAT file is being used with your game, any files in that will take priority over any you update in the original GAME archives.
- No compression is currently being applied (this doesn't make a significant difference as Dimensions hardly used any either).
- Has only been tested on Cemu, if this works on original consoles, or emulators... Let me know!