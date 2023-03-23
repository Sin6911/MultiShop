using MultiShop.Models.EShopV20;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class ShoppingCart
{
    // Lấy giỏ hàng từ Session
    public static ShoppingCart Cart
    {
        get
        {
            var cart = HttpContext.Current.Session["Cart"] as ShoppingCart;
            // Nếu chưa có giỏ hàng trong session -> tạo mới và lưu vào session
            if (cart == null)
            {
                cart = new ShoppingCart();
                HttpContext.Current.Session["Cart"] = cart;
            }
            return cart;
        }
    }
    //Download source code tại Sharecode.vn
    // Chứa các mặt hàng đã chọn
    public List<Product> Items = new List<Product>();

    public void Add(int id)
    {
        try // tìm thấy trong giỏ -> tăng số lượng lên 1
        {
            var item = Items.Single(i => i.Id == id);
            item.Quantity++;
        }
        catch // chưa có trong giỏ -> truy vấn CSDL và bỏ vào giỏ
        {
            var db = new Model1();
            var item = db.Products.Find(id);
            item.Quantity = 1;
            Items.Add(item);
        }
    }
    
    public void Remove(int id)
    {
        var item = Items.Single(i => i.Id == id);
        Items.Remove(item);
    }
    
    public void Update(int id, int newQuantity)
    {
        var item = Items.Single(i => i.Id == id);
        item.Quantity = newQuantity;
    }

    public void Clear()
    {
        Items.Clear();
    }

    public int Count
    {
        get
        {
            return Items.Count;
        }
    }
    
    public double Total
    {
        get
        {
            return Items.Sum(p => 
                p.UnitPrice * p.Quantity * (1 - p.Discount));
        }
    }
}