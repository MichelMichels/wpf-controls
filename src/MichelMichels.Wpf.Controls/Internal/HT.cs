// Copied from https://github.com/ControlzEx/ControlzEx

namespace MichelMichels.Wpf.Controls.Internal;

/// <summary>
/// Non-client hit test values, HT*
/// </summary>
internal enum HT
{
    ERROR = -2,
    TRANSPARENT = -1,
    NOWHERE = 0,
    CLIENT = 1,
    CAPTION = 2,
    SYSMENU = 3,
    GROWBOX = 4,
    MENU = 5,
    HSCROLL = 6,
    VSCROLL = 7,
    MINBUTTON = 8,
    MAXBUTTON = 9,
    LEFT = 10,
    RIGHT = 11,
    TOP = 12,
    TOPLEFT = 13,
    TOPRIGHT = 14,
    BOTTOM = 15,
    BOTTOMLEFT = 16,
    BOTTOMRIGHT = 17,
    BORDER = 18,
    OBJECT = 19,
    CLOSE = 20,
    HELP = 21
}
