<template>
  <div class="block-fileloader">
    <div>
      <label class="label">
      <slot />
      </label>
      <input
        class="input-file"
        type="file"
        accept="image/tiff"
        @change="event => readFile(event.target)"
      >
      <button
        @click="uploadFile"
        v-if="showMode">
        Загрузить
      </button>
    </div>
    <div v-if="isLoading">
      <label for="progress-bar">{{processValue}}% </label>
      <progress id="progress-bar" :value=processValue max="100"></progress>
      <button
        @click="cancelUpload"
        v-if="showMode">
        Отменить
      </button>
    </div>
  </div>
</template>

<script lang="ts">
import _ from "lodash";
import FileClient from "../API/FileClient";

import { defineComponent } from "vue";

export default defineComponent({
  props: {
    filekey: {
      type: String,
      required: true
    }
  },

  data() {
    return {
      processValue: 0,
      isLoading: false,
      fileClient: new FileClient(),
      showMode: false,
      responseData: {} as any,
      file: {} as File,
      cancelprogress: Function
    };
  },

  methods: {
    cancelUpload() {
      this.cancelprogress("The operation was canceled");
    },

    async readFile(eventTarget: any) {
      this.file = eventTarget.files[0];
      if (!this.file) {
        return;
      }

      if (!_.includes(this.file.name, this.filekey)) {
        return console.error(`Ошибка! Пожалуйста, ещё раз выберите файл с именем, содержащим ${this.filekey}.`);
      }

      try {
        this.responseData = await this.fileClient.createFile({
          filename: this.file.name,
          mimeType: this.file.type,
          lastModified: this.file.lastModified
        });

        if (!this.responseData.fileId) {
          return console.error("Не удалось загрузить файл. Попробуйте ещё раз выберите файл, пожалуйста.");
        }

        this.showMode = true;
      }
      catch (exception) {
        console.error(exception);
      }
    },

    async uploadFile() {
      this.isLoading = true;

      try {
        await this.fileClient.uploadFile(this.responseData.fileId, this.file, (value: number) =>
          this.processValue = value, (cancelprogress: FunctionConstructor) => this.cancelprogress = cancelprogress);

        this.showMode = false;
      }
      catch (error) {
        console.error(error);
      }
      finally {
        this.isLoading = false;
        this.responseData = {};
      }
    }
  }
});
</script>

<style scoped>
.block-fileloader{
  margin: 5px 0px;
}
.label{
margin: 10px;
}
</style>
