﻿namespace Astore.Domain;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<Article> Articles { get; set; }
}