<template>
  <div>
    <input
      class="input-file"
      type="file"
      accept="image/tiff"
      @change="readFile"
      multiple
    >
    <button
      @click="uploadFile"
      v-if="showMode">
      Загрузить
    </button>
    <!-- <PlatoButton v-if="showMode">
      Загрузить
    </PlatoButton> -->
  </div>
</template>

<script lang="ts">
import FileClient from "../API/FileClient";

import { AxiosError } from "axios";
import { defineComponent } from "vue";

export default defineComponent({
  data() {
    return {
      showMode: false,
      isLoading: false,
      fileClient: new FileClient(),
      fileId: "",
      file: {} as File
    };
  },

  mounted() {
    //this.fileClient = new FileClient();
  },

  methods: {
    async readFile(event: any) {
      const files = event.target.files;
      if (!files) {
        return;
      }

      this.file = files[0];

      try {
        this.fileId = await this.fileClient.createFile({
          name: this.file.name,
          type: this.file.type
        });

        this.showMode = true;
      }
      catch (error) {
        error instanceof AxiosError ? console.error(error.response?.data) : console.error(error);
      }
      finally {
        this.isLoading = false;
      }
    },

    async uploadFile() {
      if (!this.fileId) {
        return console.error("Не удалось загрузить файл. Попробуйте ещё раз загрузить файл, пожалуйста.");
      }

      try {
        await this.fileClient.uploadFile(this.fileId, this.file);
      }
      catch (error) {
        error instanceof AxiosError ? console.error(error.response?.data) : console.error(error);
      }
      finally {
        this.fileId = "",
        this.file = {} as File;
      }
    }
  }
});
</script>

<style scoped>

</style>
