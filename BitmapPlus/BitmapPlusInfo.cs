using Grasshopper;
using Grasshopper.Kernel;
using System;
using System.Drawing;

namespace BitmapPlus
{
    public class BitmapPlusInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "BitmapPlus";
            }
        }
        public override Bitmap Icon
        {
            get
            {
                //Return a 24x24 pixel bitmap to represent this GHA library.
                return Properties.Resources.BitmapPlus_Logo_24;
            }
        }
        public override string Description
        {
            get
            {
                //Return a short string describing the purpose of this GHA library.
                return "A bitmap manipulation plugin for Grasshopper 3d";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("6843af1e-15ed-49e1-baec-fd0fee4bcd4f");
            }
        }

        public override string AuthorName
        {
            get
            {
                //Return a string identifying you or your company.
                return "David Mans";
            }
        }
        public override string AuthorContact
        {
            get
            {
                //Return a string representing your preferred contact details.
                return "interopxyz@gmail.com";
            }
        }

        public override string AssemblyVersion
        {
            get
            {
                return "1.5.0.0";
            }
        }
    }

    public class BitmapPlusCategoryIcon : GH_AssemblyPriority
    {
        public object Properties { get; private set; }

        public override GH_LoadingInstruction PriorityLoad()
        {
            Instances.ComponentServer.AddCategoryIcon(Constants.ShortName, BitmapPlus.Properties.Resources.Bmp_Tab_Icon16);
            Instances.ComponentServer.AddCategorySymbolName(Constants.ShortName, 'B');
            return GH_LoadingInstruction.Proceed;
        }
    }
}