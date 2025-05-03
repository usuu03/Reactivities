using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Activities.DTO;

public class CreateActivityDTO
{
    [Required]
    public string Title {get; set;} = "";

    [Required]
    public DateTime Date {get; set;}

    [Required]
    public string Description {get; set;} = string.Empty;

    [Required]
    public string Category {get; set;} = "";
    
    //location props
    [Required]
    public  string City {get; set;} = "";

    [Required]
    public string Venue {get; set;} = "";

    [Required]
    public double Latitude {get; set;}

    [Required]
    public double Longitude {get; set;}

}
