using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace news_management.Models
{
    public class News
    {
        [Key] 
        public int id { get; set; }
        public required string title { get; set; }
        public required string content { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateOnly date { get; set; } 
    public News()
    {
    }
    
    [SetsRequiredMembers]
    public News(string title, string content, DateOnly date)
    {
        this.title = title;
        this.content = content;
        this.date = date;
    }
    }
}
