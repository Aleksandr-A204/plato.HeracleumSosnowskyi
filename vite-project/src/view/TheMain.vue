<template>
  <div class="wrapper-uploade-files">
    <FileLoader filekey="B2">
      LC08_Band2:
    </FileLoader>
    <FileLoader filekey="B3">
      LC08_Band3:
    </FileLoader>
    <FileLoader filekey="B5">
      LC08_Band5:
    </FileLoader>
  </div>
</template>

<script setup lang="ts">
import FileLoader from "@/components/FileLoader.vue";
</script>

<style scoped>
.wrapper-uploade-files{
  text-align: center;
}
</style>
