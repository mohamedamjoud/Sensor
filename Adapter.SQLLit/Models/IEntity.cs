using System.ComponentModel.DataAnnotations.Schema;

namespace Adapter.SQLLit.Models;

public interface IEntity
{ 
    public int Id { get; set; }
}