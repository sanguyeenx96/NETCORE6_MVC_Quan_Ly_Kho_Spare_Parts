using System;
using System.Collections.Generic;

namespace webspare.Models;

public partial class Danhsachlinhkien
{
    public int Id { get; set; }

    public string? Model { get; set; }

    public string? Tenjig { get; set; }

    public string? Majig { get; set; }

    public string? Tenlinhkien { get; set; }

    public string? Malinhkien { get; set; }

    public string? Maker { get; set; }

    public string? Donvi { get; set; }

    public int? Dongia { get; set; }

    public int? Tonkho { get; set; }

    public string? Ghichu { get; set; }

    public string? Image { get; set; }
}
