using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using DATPacker.Properties;

namespace DATPacker
{
    internal static class Patch
    {
        public static void CheckSet()
        {
            if (Settings.Default.PATCHLocation == "")
            {
                MessageBox.Show("If you have updated your LEGO Dimensions (i.e. v352 for Wii U) then you will need to provide the location where the update files (PATCH.DAT, PATCH1.DAT...) are included.\nIf you do not provide the location, then your files will be ignored by the game!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
