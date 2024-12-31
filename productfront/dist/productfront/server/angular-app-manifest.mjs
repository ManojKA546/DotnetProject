
export default {
  bootstrap: () => import('./main.server.mjs').then(m => m.default),
  inlineCriticalCss: true,
  baseHref: '/',
  locale: undefined,
  routes: [
  {
    "renderMode": 2,
    "route": "/"
  },
  {
    "renderMode": 2,
    "route": "/products"
  },
  {
    "renderMode": 2,
    "route": "/categories"
  },
  {
    "renderMode": 2,
    "route": "/about"
  }
],
  assets: {
    'index.csr.html': {size: 509, hash: 'bdca9143ea2cea15c4304e92e9e312f8cfb80ed137d3054bdab148410beb0239', text: () => import('./assets-chunks/index_csr_html.mjs').then(m => m.default)},
    'index.server.html': {size: 1022, hash: '2681e3010e00c4689035c1178f4555177be536a4d05983d9a0471d6bc5980718', text: () => import('./assets-chunks/index_server_html.mjs').then(m => m.default)},
    'products/index.html': {size: 5612, hash: '85e4f142699dd1f707c3d0b116226a26e82df38ec3fd5ab36ebe8787fd735416', text: () => import('./assets-chunks/products_index_html.mjs').then(m => m.default)},
    'categories/index.html': {size: 4274, hash: '61823019a7ea9659d016adaad956208a159d987d594a0699e73b79e7c38c48ff', text: () => import('./assets-chunks/categories_index_html.mjs').then(m => m.default)},
    'about/index.html': {size: 3530, hash: '00588112cfc6cd2b857fb24dc92a9010459942f00691b86596f4addfb860545c', text: () => import('./assets-chunks/about_index_html.mjs').then(m => m.default)},
    'index.html': {size: 3488, hash: 'ae1d34aa1d6aa7754c88b09faff8b17d455c6b82f4db3c1f8517bd6c91e0f98d', text: () => import('./assets-chunks/index_html.mjs').then(m => m.default)},
    'styles-5INURTSO.css': {size: 0, hash: 'menYUTfbRu8', text: () => import('./assets-chunks/styles-5INURTSO_css.mjs').then(m => m.default)}
  },
};
