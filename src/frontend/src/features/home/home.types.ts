import type { RouteLocationRaw } from 'vue-router'
import type { AppIconName } from '../../components/ui/icon.types'

export interface HomeAction {
  id: string
  label: string
  icon: AppIconName
  to: RouteLocationRaw
}

export interface PortfolioItem {
  id: string
  imageUrl: string
  alt: string
}

export interface PhotographyCategory {
  id: string
  label: string
  icon: AppIconName
  to: RouteLocationRaw
}

export interface LastOrder {
  id: string
  studioName: string
  dateLabel: string
  statusLabel: string
  newPhotoCount: number
  thumbnailUrl: string
  to: RouteLocationRaw
}

export interface AlbumPreviewItem {
  id: string
  title: string
  caption: string
  accent: 'mint' | 'pink' | 'lilac'
  to: RouteLocationRaw
}
