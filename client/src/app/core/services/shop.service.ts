import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Product } from '../../shared/models/product';
import { Pagination } from '../../shared/models/pagination';
import { ShopParams } from '../../shared/models/shopParams';

@Injectable({
  providedIn: 'root', // at boot it is provided to all of our components
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/';
  private http = inject(HttpClient);
  types: string[] = [];
  brands: string[] = [];

  getProducts(shopParams: ShopParams) {
    let params = new HttpParams();
    let { brands, types, sort, search } = shopParams;

    if (brands && brands.length > 0) {
      params = params.append('brands', brands.join(','));
    }
    if (types && types.length > 0) {
      params = params.append('types', types.join(','));
    }
    if (sort) {
      params = params.append('sort', sort);
    }
    if (search) {
      params = params.append('search', search);
    }

    params = params.append('pageSize', shopParams.pageSize);
    params = params.append('pageIndex', shopParams.pageNumber);

    return this.http.get<Pagination<Product>>(this.baseUrl + 'products', {
      params,
    });
  }

  getBrands() {
    if (this.brands.length > 0) return;
    return this.http.get<string[]>(this.baseUrl + 'products/brands').subscribe({
      next: (response) => (this.brands = response),
    });
  }

  getTypes() {
    if (this.types.length > 0) return;
    return this.http.get<string[]>(this.baseUrl + 'products/types').subscribe({
      next: (response) => (this.types = response),
    });
  }
}
