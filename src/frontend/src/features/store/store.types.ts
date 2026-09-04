export type StoreCategory =
  | 'all'
  | 'newborn-albums'
  | 'child-albums'
  | 'luxury-albums'
  | 'frames'
  | 'prints'
  | 'other'

export type StoreSort = 'newest' | 'bestselling' | 'price-asc' | 'price-desc'

export interface StoreCategoryOption {
  id: StoreCategory
  label: string
  icon: 'all' | 'album' | 'frame' | 'print' | 'gift'
}

export interface StoreProduct {
  id: string
  title: string
  subtitle: string
  category: Exclude<StoreCategory, 'all'>
  imageUrl: string
  imageAlt: string
  price: number
  oldPrice?: number
  available: boolean
  createdAt: string
  sales: number
}

export type ProductTabId = 'specifications' | 'overview' | 'reviews'

export interface ProductGalleryImage {
  src: string
  alt: string
}

export interface ProductSpecification {
  label: string
  value: string
}

export interface ProductReview {
  id: string
  author: string
  date: string
  rating: number
  title?: string
  body: string
  likes: number
  dislikes: number
  variant: string
}

export interface StoreProductDetails {
  product: StoreProduct
  categoryLabel: string
  tagline: string
  price: number
  introduction: string
  overview: string
  gallery: ProductGalleryImage[]
  specifications: ProductSpecification[]
  benefits: string[]
  rating: number
  reviewCount: number
  ratingDistribution: Array<{ stars: number; percent: number }>
  reviews: ProductReview[]
}
