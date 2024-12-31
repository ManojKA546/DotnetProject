import { Component, OnInit } from '@angular/core';
import { Category } from '../../interfaces/category';
import { CategoryService } from '../../services/category.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  standalone: true,
  selector: 'app-categories',
  imports: [CommonModule, FormsModule],
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent implements OnInit {
  categories: Category[] = [];
  newCategory: Category = { categoryId: 0, name: '' };
  editMode: boolean = false;
  currentCategoryId: number | null = null;
  isLoading: boolean = false;  // For managing loading state
  errorMessage: string | null = null; // For displaying error messages

  constructor(private categoryService: CategoryService) {}

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories(): void {
    this.isLoading = true;
    this.categoryService.getCategories().subscribe(
      (data) => {
        this.categories = data;
        this.isLoading = false;
      },
      (error) => {
        this.isLoading = false;
        this.errorMessage = 'Error loading categories. Please try again later.';
      }
    );
  }

  addCategory(): void {
    this.isLoading = true;
    this.categoryService.addCategory(this.newCategory).subscribe(
      () => {
        this.loadCategories();
        this.resetForm();
      },
      (error) => {
        this.isLoading = false;
        this.errorMessage = 'Error adding category. Please try again later.';
      }
    );
  }

  deleteCategory(categoryId: number): void {
    this.isLoading = true;
    this.categoryService.deleteCategory(categoryId).subscribe(
      () => {
        this.loadCategories();
      },
      (error) => {
        this.isLoading = false;
        this.errorMessage = 'Error deleting category. Please try again later.';
      }
    );
  }

  updateCategory(category: Category): void {
    if (this.currentCategoryId) {
      this.newCategory.categoryId = this.currentCategoryId;
      this.isLoading = true;
      this.categoryService.updateCategory(this.newCategory).subscribe(
        () => {
          this.loadCategories();
          this.resetForm();
        },
        (error) => {
          this.isLoading = false;
          this.errorMessage = 'Error updating category. Please try again later.';
        }
      );
    }
  }

  editCategory(category: Category): void {
    this.editMode = true;
    this.currentCategoryId = category.categoryId;
    this.newCategory = { ...category };
  }

  resetForm(): void {
    this.editMode = false;
    this.currentCategoryId = null;
    this.newCategory = { categoryId: 0, name: '' };
  }
}
