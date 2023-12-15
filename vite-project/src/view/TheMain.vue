<template>
  <div class="wrapper-uploade-files">
    <FileLoader filekey="B2" >
      LC08_Band2:
    </FileLoader>
    <FileLoader filekey="B3" >
      LC08_Band3:
    </FileLoader>
    <FileLoader filekey="B5" >
      LC08_Band5:
    </FileLoader>
    <button @click="discoverHeracleumSosnowskyi">
      Обнаружить борщевик Сосновского
    </button>
  </div>
</template>

<script  lang="ts">
import FileLoader from "@/components/FileLoader.vue";

import { defineComponent } from "vue";

export default defineComponent({
  components: {
    FileLoader
  },

  methods: {
    discoverHeracleumSosnowskyi() {

    }
  }
});
</script>

<style scoped>
.wrapper-uploade-files{
  text-align: center;
}
</style>
