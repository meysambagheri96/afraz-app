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
