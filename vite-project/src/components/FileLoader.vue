<template>
  <div>
    <input
      class="input-file"
      type="file"
      accept="image/tiff"
      @change="readFile"
    >
    <button
      @click="uploadFile"
      v-if="showMode">
      Загрузить
    </button>
    <button @click="downloadFile">
      Скачать
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
      responseData: {} as any,
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
        this.responseData = await this.fileClient.createFile({
          fileName: this.file.name,
          type: this.file.type,
          lastModified: this.file.lastModified
        });

        this.showMode = true;
      }
      catch (exception) {
        exception instanceof AxiosError ? console.error(exception.response?.data) : console.error(exception);
      }
      finally {
        this.isLoading = false;
      }
    },

    async uploadFile() {
      if (!this.responseData.fileId) {
        return console.error("Не удалось загрузить файл. Попробуйте ещё раз выберите файл, пожалуйста.");
      }

      try {
        await this.fileClient.uploadFile(this.responseData.fileId, this.file);
      }
      catch (error) {
        error instanceof AxiosError ? console.error(error.response?.data) : console.error(error);
      }
      finally {
        this.responseData = {},
        this.file = {} as File;

        this.showMode = false;
      }
    },

    async downloadFile() {
      await this.fileClient.downloadFile();
    }
  }
});
</script>

<style scoped>

</style>
