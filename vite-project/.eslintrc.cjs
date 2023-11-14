/* eslint-env node */
require("@rushstack/eslint-patch/modern-module-resolution");

module.exports = {
  root: true,
  "extends": [
    "plugin:vue/vue3-essential",
    "eslint:recommended",
    "@vue/eslint-config-typescript",
    "@vue/eslint-config-prettier/skip-formatting"
  ],
  parserOptions: {
    ecmaVersion: "latest"
  },
  plugins: [
    "sort-imports-es6-autofix"
  ],
  rules: {
    // "no-debugger": "off",
    "brace-style": ["error", "stroustrup"],
    "curly": ["error", "all"],
    "semi": ["error", "always"],
    "indent": ["error", 2],
    "max-len": ["warn", { "code": 500 }],
    "quotes": ["error", "double"],
    "arrow-parens": ["error", "as-needed"],
    "space-in-parens": ["error", "never"],
    "array-bracket-spacing": ["error", "never"],
    "object-curly-spacing": ["error", "always"],
    "space-before-function-paren": ["error", { "anonymous": "never", "named": "never", "asyncArrow": "always" }],
    "comma-spacing": ["error", { "before": false, "after": true }],
    "comma-dangle": ["error", "never"],
    "@typescript-eslint/no-unused-vars": ["error", { "argsIgnorePattern": "^_" }],
    "space-before-blocks": "error",
    "keyword-spacing": ["error", { "before": true, "after": true }],
    "space-infix-ops": "error",
    "no-trailing-spaces": "error",
    "no-var": "error",
    "no-multi-spaces": "error",
    "key-spacing": "error",
    "padded-blocks": ["error", { "blocks": "never" }],
    "vue/no-mutating-props": "warn",
    "vue/multi-word-component-names": "off",
    "vue/no-computed-properties-in-data": "warn",
    "sort-imports-es6-autofix/sort-imports-es6": ["error", {
      "ignoreCase": true,
      "ignoreMemberSort": false,
      "memberSyntaxSortOrder": ["all", "single", "multiple", "none"]
    }],
    "vue/component-name-in-template-casing": ["error", "PascalCase", {
      "registeredComponentsOnly": false,
      "ignores": []
    }]
  }
};
