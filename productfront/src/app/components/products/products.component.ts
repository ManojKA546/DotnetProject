import { Component, OnInit } from '@angular/core';
import { Product } from '../../interfaces/product';
import { ProductService } from '../../services/product.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';


@Component({
  standalone: true,
  selector: 'app-products',
  imports: [CommonModule,FormsModule],
  templateUrl: './products.component.html',
  styleUrl: './products.component.css'
})
export class ProductsComponent implements OnInit {

  products: Product[] = [];

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.productService.getProducts().subscribe((data) => {
      this.products = data;
    });

}

newProduct: Product = {
  productId: 0,
  name: '',
  salesPrice: 0,
  mrp: 0,
  categoryId: 0,
};

editMode: boolean = false;  // Flag to track edit mode
  currentProductId: number = 0;  // To track which product is being edited


addProduct(): void {
  this.productService.addProduct(this.newProduct).subscribe(() => {
    this.loadProducts();
    this.newProduct = { productId: 0, name: '', salesPrice: 0, mrp: 0, categoryId: 0 }; // Reset the form
  });
}

deleteProduct(productId: number): void {
  this.productService.deleteProduct(productId).subscribe(() => {
    this.loadProducts(); // Refresh the list after deletion
  });
}


editProduct(product: Product): void {
  this.newProduct = { ...product };  // Pre-fill form with the product to edit
  this.editMode = true;  // Set edit mode to true
  this.currentProductId = product.productId;  // Store the current product's ID
}

updateProduct(): void {
  this.productService.updateProduct(this.newProduct).subscribe(() => {
    this.loadProducts();  // Refresh the list after updating the product
    this.resetForm();  // Reset the form after updating the product
  });
}

resetForm(): void {
  this.newProduct = { productId: 0, name: '', salesPrice: 0, mrp: 0, categoryId: 0 };
  this.editMode = false;  // Reset edit mode
  this.currentProductId = 0;  // Reset current product ID
}

}


