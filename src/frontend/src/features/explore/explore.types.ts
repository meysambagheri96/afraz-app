export type ExploreCategory = 'all' | 'newborn' | 'child' | 'birthday' | 'pregnancy' | 'family' | 'outdoor'
export type ExploreSort = 'newest' | 'oldest' | 'popular'

export interface ExplorePhoto {
  id: string
  src: string
  category: Exclude<ExploreCategory, 'all'>
  alt: string
  createdAt: string
  popularity: number
  position: string
}

export interface ExploreCategoryOption {
  id: ExploreCategory
  label: string
  icon?: Exclude<ExploreCategory, 'all'>
}
