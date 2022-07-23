# Pre-requisites

Ensure you have the following installed:

- [.NET 6.0 x64](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-6.0.7-windows-x64-installer) __**AND**__ [.NET 6.0 x86](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-6.0.7-windows-x86-installer)

You need both the x86 and x64 variants of .NET 6 installed, otherwise files will not extract correctly.

And download DATPacker:

- [DATPacker](https://github.com/connorh315/DATPacker/releases/latest)

- 30GB of additional hard drive space (15 GB for the extracted files and 15 GB for the new archives)

# Steps

1. Open DATPacker, click on Project->New and fill in all fields explained below:

- Project name: A name for your project, could be anything from "My Dimensions Project" to "Stranger Things Mod"
- DAT archive folder: This is the folder containing all of your GAME.DAT files (should be GAME.DAT through to GAME9.DAT)
- Build folder: This is where DATPacker will build your archives to. I'd suggest that you set this to be where CEMU holds the GAME.DAT files, that way you won't need to move them every time you rebuild them.
- Update folder: This is the folder containing all of your PATCH.DAT files (These are update files for the game, it is very important that you include this folder if you have the updates running, otherwise Dimensions will refuse your custom files).
- Output folder: This is where DATPacker will extract all of your files to, you should probably choose an empty folder for this.

2. Once you have entered all of the project info, click on "Create project". This will begin extracting all of the files into your chosen Output folder and will also place a project file (.hprj) there too.

3. Wait until you receive a message that all archives have been extracted. You could wait a few minutes on an NVME drive, up to 10 minutes on a SATA SSD, or up to 20 minutes on the average hard drive.

4. If your PATCH.DAT archives were inside of a CEMU game directory, move them out now. If you are unsure, go to your CEMU directory, and search for PATCH.DAT. If you have it, move it to your Desktop or somewhere safe.

5. When prompted, rebuild all archives. This will place the archives in your chosen Build folder, so move them to the CEMU game directory if necessary.

6. Wait for the rebuild to complete, and then open CEMU and test to make sure everything still works. If it does, we can do one final test to see if CEMU is now using our custom archives:

7. Edit chars\collection.txt and swap the portal_model_id of two characters. I changed Wyldstyle's portal_model_id 3 to be portal_model_id 21, and then changed Homer Simpson's portal_model_id 21 to be portal_model_id 3. This means that placing Wyldstyle on the portal should spawn Homer, and vice versa.

If you could get Homer Simpson to show up instead of Wyldstyle, then congrats, you're free to start modding, just read the section "Important Notes" below though.

And if you couldn't get it to work, here's what you need to do:
- First, make sure you read every step properly.
- Then, read the "Important Notes" to make sure you're not doing anything silly
- Then, read the Console (the black box) and see if it gives any important information that may help figure out what's going wrong.
- Finally, ask for help in #tools_help on the TTGames Lego Modding server: https://discord.gg/9gYXPka

# Important notes

You must keep DATPacker open while modding, the whole way this works is that DATPacker watches over everything you change about your output folder and tells you which GAME.DAT archive you will need to rebuild because of it.

Should you accidentally edit a file while DATPacker is not open here's what you need to do:
- If you edited it, edit it again with DATPacker open, this will tell you which archive needs to be rebuilt.
- If you created a file, close DATPacker, move it out of the build folder, open DATPacker and move it back to where you want it.
- If you deleted a file, close DATPacker, create a temporary file with the same name and location, open DATPacker and then delete that file.
- If you've made an absolute mess of it and aren't sure what you did, click on Files->Resynchronise, this should find any deletions/creations that DATPacker doesn't know about. If you've edited so many files that you can't remember which ones were edited, just click on Build->Build all archives. It shouldn't take more than 10 minutes on an SSD and will be a lesson in being orderly.

No compression methods are currently applied, so new DAT files will be bigger than the originals shipped with the game. If the new archives go over 3GB, they may be unusable, this will be fixed in the future.

When you find a bug, let me know! Do it either by asking about it in the Discord server linked above, or by starting an Issue on this GitHub.

This is a BETA at best, don't expect it to work.

Good luck.