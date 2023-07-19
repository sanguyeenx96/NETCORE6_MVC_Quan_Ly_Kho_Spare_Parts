using System;
using System.Collections.Generic;

namespace webspare.Models;

public partial class LichsuLaylinhkien
{
    public int Id { get; set; }

    public string? Tenjig { get; set; }

    public string? Majig { get; set; }

    public string? Tenlinhkien { get; set; }

    public string? Malinhkien { get; set; }

    public string? Maker { get; set; }

    public string? Donvi { get; set; }

    public int? Soluong { get; set; }

    public string? Nguoilay { get; set; }

    public DateTime? Ngaylay { get; set; }

    public string? Image { get; set; }

    public string? Model { get; set; }
}
