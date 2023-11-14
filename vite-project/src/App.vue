<template>
  <div>
    <button><RouterLink :to="{name: 'main'}">Main</RouterLink></button>
    <button><RouterLink :to="{name: 'about'}">About</RouterLink></button>
  </div>
  <RouterView />
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { RouterLink, RouterView } from "vue-router";

export default defineComponent({
  components: {
    RouterLink,
    RouterView
  }
});
</script>

<style scoped>

</style>
