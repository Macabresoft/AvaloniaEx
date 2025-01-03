namespace Macabresoft.AvaloniaEx.Sample.Models;

using System;
using System.ComponentModel.DataAnnotations;

[Flags]
public enum FakeFlagsEnum {
    None = 0,

    [Display(Name = "1st")]
    First = 1 << 0,

    [Display(Name = "2nd")]
    Second = 1 << 1,

    [Display(Name = "3rd")]
    Third = 1 << 2,

    [Display(Name = "4th")]
    Fourth = 1 << 3,

    [Display(Name = "5th")]
    Fifth = 1 << 4,

    [Display(Name = "6th")]
    Sixth = 1 << 5,

    [Display(Name = "7th")]
    Seventh = 1 << 6,

    [Display(Name = "8th")]
    Eighth = 1 << 7,
    Ninth = 1 << 8,
    Tenth = 1 << 9,
    Eleventh = 1 << 10,
    Twelfth = 1 << 11,
    Thirteenth = 1 << 12,
    Fourteenth = 1 << 13,
    Fifteenth = 1 << 14,
    Sixteenth = 1 << 15,
    Seventeenth = 1 << 16,
    Eighteenth = 1 << 17,
    Nineteenth = 1 << 18,
    Twentieth = 1 << 19
}