import type { Component } from 'vue'

export interface ProfileMenuItem {
  id: string
  title: string
  subtitle?: string
  icon: Component
  destructive?: boolean
}

export interface CustomerProfile {
  name: string
  mobile: string
  membership: string
  avatarUrl: string
  avatarAlt: string
  isAdmin: boolean
}
