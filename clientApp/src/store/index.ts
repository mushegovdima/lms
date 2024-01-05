import { createStore, type Store } from 'vuex';
import { auth } from './auth.module';
import type { InjectionKey } from 'vue';

export const key: InjectionKey<Store<unknown>> = Symbol()

const store = createStore({
  modules: {
    auth,
  },
});

export default store;