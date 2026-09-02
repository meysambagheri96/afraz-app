import sampleOneUrl from '../../assets/images/photography/home/sample-1.png'
import sampleTwoUrl from '../../assets/images/photography/home/sample-2.png'
import sampleThreeUrl from '../../assets/images/photography/home/sample-3.png'
import type { ExploreCategoryOption, ExplorePhoto } from './explore.types'

export const exploreCategories: readonly ExploreCategoryOption[] = [
  { id: 'all', label: 'همه' },
  { id: 'newborn', label: 'نوزاد', icon: 'newborn' },
  { id: 'child', label: 'کودک', icon: 'child' },
  { id: 'birthday', label: 'تولد', icon: 'birthday' },
  { id: 'pregnancy', label: 'بارداری', icon: 'pregnancy' },
  { id: 'family', label: 'خانوادگی', icon: 'family' },
  { id: 'outdoor', label: 'فضای باز', icon: 'outdoor' },
]

const seeds = [
  { src: sampleOneUrl, category: 'newborn', alt: 'نوزاد در دکور روشن آتلیه' },
  { src: sampleTwoUrl, category: 'child', alt: 'کودک همراه خرس عروسکی در فضای باز' },
  { src: sampleThreeUrl, category: 'birthday', alt: 'پرتره کودک در دکور جشن تولد' },
  { src: sampleThreeUrl, category: 'family', alt: 'پرتره خانوادگی کودک در دکور گل' },
  { src: sampleOneUrl, category: 'pregnancy', alt: 'عکاسی بارداری با نور طبیعی' },
  { src: sampleTwoUrl, category: 'outdoor', alt: 'پرتره کودک در فضای باز' },
] as const

const positions = ['center', '45% center', '58% center', 'center 38%', 'center 62%']

export const explorePhotos: readonly ExplorePhoto[] = Array.from({ length: 48 }, (_, index) => {
  const seed = seeds[index % seeds.length]
  return {
    id: `explore-${index + 1}`,
    src: seed.src,
    category: seed.category,
    alt: seed.alt,
    createdAt: new Date(2026, 7, 31 - index).toISOString(),
    popularity: 100 - ((index * 17) % 83),
    position: positions[index % positions.length],
  }
})
