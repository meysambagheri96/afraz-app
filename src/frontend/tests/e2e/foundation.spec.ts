import { expect, test } from '@playwright/test'

test('shows the Afraz foundation', async ({ page }) => {
  await page.goto('/')

  await expect(page.getByRole('heading', { name: 'استودیو افراز' })).toBeVisible()
  await expect(page.locator('html')).toHaveAttribute('dir', 'rtl')
})
