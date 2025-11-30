using InnoShop.ProductManagment.Domain.Exceptions;
using InnoShop.ProductManagment.Domain.Models;
using System;

namespace InnoShop.ProductManagment.Domain.Models;

public class Product : Entity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public double Price { get; private set; }
    public bool IsAvailable { get; private set; }
    public Guid UserId { get; init; }
    public DateTime CreatedAt { get; init; }
    public bool IsDeleted { get; private set; }

    private Product() { }

    public Product(string name, string description, Guid userId, double price)
    {
        Name = name;
        Description = description;
        IsAvailable = true;
        UserId = userId;
        CreatedAt = DateTime.UtcNow;
        IsDeleted = false;
        Price = price;
    }

    public void ChangeName(string name)
    {

        Name = name;
    }

    public void ChangeDescription(string description)
    {
       
        Description = description;
    }

    public void ChangePrice(double price)
    {
        Price = price;
    }

    public void SetAvailable()
    {
        if (IsAvailable)
        {
            throw new AlreadyDoneException("");
        }
        IsAvailable = true;
    }

    public void SetUnavailable()
    {
        if (!IsAvailable)
        {
            throw new AlreadyDoneException("");
        }
        IsAvailable = false;
    }

    public void Delete()
    {
        if (IsDeleted)
        {
            throw new AlreadyDoneException("");
        }
        IsDeleted = true;
    }

    public void Recover()
    {
        if (!IsDeleted)
        {
            throw new AlreadyDoneException("");
        }
        IsDeleted = false;
    }
}