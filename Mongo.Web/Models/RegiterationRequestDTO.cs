﻿using System.ComponentModel.DataAnnotations;

namespace Mongo.Web.Models;

public class RegiterationRequestDto
{
    [Required]
    public string Email { get; set; }
    [Required]

    public string Name { get; set; }
    [Required]

    public string PhoneNumber { get; set; }
    [Required]

    public string Password { get; set; }
    public string? Rolename { get; set; }

}