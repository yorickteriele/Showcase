public class WorkExperience {
    public string Title { get; set; }
    public string Period { get; set; }
    public string Institute { get; set; }

    public WorkExperience(string title, string period, string institute) {
        Title = title;
        Period = period;
        Institute = institute;
    }
}