<template>
  <div class="main-content">
    <div class="tab-switch-menu">
        <div class="tab-switch-menu__item"></div>
        <router-link to="/user/create" class="tab-switch-menu__item">Profile</router-link>
        <router-link to="/user/edit/role" class="tab-switch-menu__item">User role</router-link>
        <router-link to="/user/edit/settings" class="tab-switch-menu__item">Settings</router-link>
    </div>
    <div class="tab-content">
        <router-view></router-view>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Content',
  components: {
  },
  data () {
    return {
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style lang="scss" scoped>
    .main-content {
        width: 100%;

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
