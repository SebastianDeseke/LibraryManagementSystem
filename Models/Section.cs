using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Models;

public class Section {
    //A section is used to orden shelves under the same cateegory (Author or Genre?)
    public char SecId { get; set; }
    public string SectionName { get; set; }
    public string SectionDescription { get; set; }
    public ICollection<Shelve> Shelves { get; set; }

    public Section (string SectionName, string SectionDescription) {
        this.SectionName = SectionName;
        this.SectionDescription = SectionDescription;
    }
}