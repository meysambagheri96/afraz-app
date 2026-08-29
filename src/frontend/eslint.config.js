import eslint from '@eslint/js'
import { defineConfigWithVueTs, vueTsConfigs } from '@vue/eslint-config-typescript'
import vue from 'eslint-plugin-vue'

export default defineConfigWithVueTs(
  { ignores: ['node_modules/**', '../backend/Afraz.Api/wwwroot/**'] },
  eslint.configs.recommended,
  vue.configs['flat/recommended'],
  vueTsConfigs.recommended,
)
