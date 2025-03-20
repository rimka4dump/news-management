<script setup>
import { RouterView } from 'vue-router'
</script>

<template>
  <RouterView/>
</template>

<style scoped>
header {
  line-height: 1.5;
}

#app{
  margin: 0 !important;
  padding: 0 !important;
}

.logo {
  display: block;
  margin: 0 auto 2rem;
}

.main{
  margin: 0 !important;
  padding: 0 !important;
}

@media (min-width: 1024px) {
  header {
    display: flex;
    place-items: center;
  }

  header .wrapper {
    display: flex;
    place-items: flex-start;
    flex-wrap: wrap;
  }
}
</style>
