using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Models;

public class Section {
    //A section is used to orden shelves under the same cateegory (Author or Genre?)
    [Key]
    public char Id { get; set; }
    public string SectionName { get; set; }
    public string SharedTheme { get; set; }
    public string SectionDescription { get; set; }
    public ICollection<Shelf> Shelves { get; set; }

    public Section (string SectionName, string SectionDescription, string SharedTheme) {
        this.Id = MakeSecID();
        this.SectionName = SectionName;
        this.SectionDescription = SectionDescription;
        this.SharedTheme = SharedTheme;
    }

    public void AddShelve(Shelf shelve) {
        Shelves.Add(shelve);
    }

    public void RemoveShelve(Shelf shelve) {
        Shelves.Remove(shelve);
    }

    public char MakeSecID(){
        //Check the existong Sections in DB and make a new ID
        return 'A';
    }
}