<template>
  <div class="main-content">
    <div class="tab-switch-menu">
        <div class="tab-switch-menu__item"></div>
        <router-link v-bind:to="profileLinkUrl" class="tab-switch-menu__item">Profile</router-link>
        <router-link v-if="shared.editableUserProfile.id" to="/user/edit/role" class="tab-switch-menu__item">User role</router-link>
        <router-link v-if="shared.editableUserProfile.id" to="/user/edit/settings" class="tab-switch-menu__item">Settings</router-link>
    </div>
    <div class="tab-content">
        <router-view></router-view>
    </div>
  </div>
</template>

<script>
import store from '../store.js';

export default {
  name: 'Content',
  components: {
  },
  data () {
    return {
        shared: store.state,
        // userProfileId: this.$route.params.id
    }
  },
  computed: {
      profileLinkUrl: function() {
          let id = this.$route.params.id;
          let url = !!id ? `/user/edit/profile/${id}` : '/user/create';
          return url;
      }
  }
}
</script>

<style lang="scss" scoped>
    .main-content {
        width: 100%;
        max-height: 100vh;
        overflow-y: auto;

        .tab-switch-menu {
            text-align: center;
            padding: 0.5rem 1rem;
            background-color: #585858;

            .tab-switch-menu__item {
                color: #fff;
                display: inline-block;
                text-decoration: none;

                &:not(:last-child) {
                    margin-right: 1rem;
                }
                &.router-link-active {
                    border-bottom: 2px solid #8db0ff;
                }
            }
        }

        .tab-content {
            padding: 3rem 5rem;
        }
    }
</style>
