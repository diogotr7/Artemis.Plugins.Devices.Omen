﻿using RGB.NET.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGB.NET.Devices.Omen
{
    public static class LedMapping
    {
        public static LedMapping<int> Sequencer { get; } = new()
        {
            [LedId.Keyboard_Escape] = 1,
            [LedId.Keyboard_F1] = 2,
            [LedId.Keyboard_F2] = 3,
            [LedId.Keyboard_F3] = 4,
            [LedId.Keyboard_F4] = 5,
            [LedId.Keyboard_F5] = 6,
            [LedId.Keyboard_F6] = 7,
            [LedId.Keyboard_F7] = 8,
            [LedId.Keyboard_F8] = 9,
            [LedId.Keyboard_F9] = 10,
            [LedId.Keyboard_F10] = 11,
            [LedId.Keyboard_F11] = 12,
            [LedId.Keyboard_F12] = 13,
            [LedId.Keyboard_PrintScreen] = 14,
            [LedId.Keyboard_PauseBreak] = 16,
            [LedId.Keyboard_ScrollLock] = 15,
            [LedId.Keyboard_GraveAccentAndTilde] = 17,
            [LedId.Keyboard_1] = 18,
            [LedId.Keyboard_2] = 19,
            [LedId.Keyboard_3] = 20,
            [LedId.Keyboard_4] = 21,
            [LedId.Keyboard_5] = 22,
            [LedId.Keyboard_6] = 23,
            [LedId.Keyboard_7] = 24,
            [LedId.Keyboard_8] = 25,
            [LedId.Keyboard_9] = 26,
            [LedId.Keyboard_0] = 27,
            [LedId.Keyboard_MinusAndUnderscore] = 28,
            [LedId.Keyboard_EqualsAndPlus] = 29,
            [LedId.Keyboard_Backspace] = 30,
            [LedId.Keyboard_Insert] = 31,
            [LedId.Keyboard_Home] = 32,
            [LedId.Keyboard_PageUp] = 33,
            [LedId.Keyboard_Tab] = 38,
            [LedId.Keyboard_Q] = 39,
            [LedId.Keyboard_W] = 40,
            [LedId.Keyboard_E] = 41,
            [LedId.Keyboard_R] = 42,
            [LedId.Keyboard_T] = 43,
            [LedId.Keyboard_Y] = 44,
            [LedId.Keyboard_U] = 45,
            [LedId.Keyboard_I] = 46,
            [LedId.Keyboard_O] = 47,
            [LedId.Keyboard_P] = 48,
            [LedId.Keyboard_BracketLeft] = 49,
            [LedId.Keyboard_BracketRight] = 50,
            [LedId.Keyboard_Backslash] = 51,
            [LedId.Keyboard_Delete] = 52,
            [LedId.Keyboard_End] = 53,
            [LedId.Keyboard_PageDown] = 54,
            [LedId.Keyboard_CapsLock] = 59,
            [LedId.Keyboard_A] = 60,
            [LedId.Keyboard_S] = 61,
            [LedId.Keyboard_D] = 62,
            [LedId.Keyboard_F] = 63,
            [LedId.Keyboard_G] = 64,
            [LedId.Keyboard_H] = 65,
            [LedId.Keyboard_J] = 66,
            [LedId.Keyboard_K] = 67,
            [LedId.Keyboard_L] = 68,
            [LedId.Keyboard_SemicolonAndColon] = 69,
            [LedId.Keyboard_ApostropheAndDoubleQuote] = 70,
            [LedId.Keyboard_Enter] = 72,
            [LedId.Keyboard_LeftShift] = 76,
            [LedId.Keyboard_Z] = 78,
            [LedId.Keyboard_X] = 79,
            [LedId.Keyboard_C] = 80,
            [LedId.Keyboard_V] = 81,
            [LedId.Keyboard_B] = 82,
            [LedId.Keyboard_N] = 83,
            [LedId.Keyboard_M] = 84,
            [LedId.Keyboard_CommaAndLessThan] = 85,
            [LedId.Keyboard_PeriodAndBiggerThan] = 86,
            [LedId.Keyboard_SlashAndQuestionMark] = 87,
            [LedId.Keyboard_RightShift] = 88,
            [LedId.Keyboard_ArrowUp] = 89,
            [LedId.Keyboard_LeftCtrl] = 94,
            [LedId.Keyboard_LeftGui] = 95,
            [LedId.Keyboard_LeftAlt] = 96,
            [LedId.Keyboard_Space] = 97,
            [LedId.Keyboard_RightAlt] = 98,
            [LedId.Keyboard_RightGui] = 99,
            [LedId.Keyboard_Function] = 107,
            [LedId.Keyboard_RightCtrl] = 101,
            [LedId.Keyboard_ArrowLeft] = 102,
            [LedId.Keyboard_ArrowDown] = 103,
            [LedId.Keyboard_ArrowRight] = 104
        };
    }
}
