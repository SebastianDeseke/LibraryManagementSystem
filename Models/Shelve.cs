using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentSystem.Models;

public class Shelve {
    //A shelve is used to orden books under the same cateegory (Author or Genre?)
    public int ShelveId { get; set; }
    public string? ShelveName { get; set; }
    public ICollection<Book> Books { get; set; }
    public string? SharingAttribute { get; set; }
    public Section Section { get; set; }

    public Shelve (string? ShelveName, string? SharingAttribute, Section Section) {
        this.ShelveName = ShelveName;
        this.SharingAttribute = SharingAttribute;
        this.Section = Section;
    }
}