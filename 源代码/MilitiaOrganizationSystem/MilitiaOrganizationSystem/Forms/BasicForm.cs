﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevComponents.DotNetBar;
using System.Runtime.InteropServices;

namespace MilitiaOrganizationSystem
{
    public class BasicForm : Office2007Form
    {//为了改变显示样式，所有界面继承于此BasicForm
        [DllImport("user32.dll", EntryPoint = "AnimateWindow")]
        protected static extern bool AnimateWindow(IntPtr handle, int ms, int flags);
        public const Int32 AW_HOR_POSITIVE = 0x00000001;
        public const Int32 AW_HOR_NEGATIVE = 0x00000002;
        public const Int32 AW_VER_POSITIVE = 0x00000004;
        public const Int32 AW_VER_NEGATIVE = 0x00000008;
        public const Int32 AW_CENTER = 0x00000010;
        public const Int32 AW_HIDE = 0x00010000;
        public const Int32 AW_ACTIVATE = 0x00020000;
        public const Int32 AW_SLIDE = 0x00040000;
        public const Int32 AW_BLEND = 0x00080000;

        public BasicForm()
        {
            EnableGlass = false;
        }
    }
}
