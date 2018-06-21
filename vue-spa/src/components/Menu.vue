<template>
  <div class="menu">
    <router-link to="/user/create" class="add-user-btn">
        <i class="fas fa-plus add-user-btn__icon"></i>
        <span class="add-user-btn__text">Add user</span>
    </router-link>
    <div class="search-box">
        <i class="fas fa-search search-icon"></i>
        <input type="text" class="search-input">
    </div>
    <div class="filters">
        <div class="filter-item filter-item--active">Enabled</div>
        <div class="filter-item">Disabled</div>
    </div>
    <div class="user-list">
        <div class="user-item">
            <router-link to="/user/edit/1">
                <div class="user-item__image">
                    <img src="https://angular.io/assets/images/logos/angular/logo-nav@2x.png" />
                </div>
            </router-link>
            <router-link to="/user/edit/1">
                <div class="user-item__name">Test</div>
            </router-link>
        </div>
    </div>
  </div>
  
</template>

<script>
export default {
  name: 'Menu',
  data () {
    return {
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style lang="scss" scoped>
    .menu {
      width: 25%;
      min-width: 200px;
      max-width: 300px;
      min-height: 100vh;
      background-color: #f1f1f1;

      .add-user-btn {
        margin: 1rem;
        cursor: pointer;
        display: block;

        &:hover {
          color: rgb(97, 97, 207);
        }

        .add-user-btn__icon {
          margin-right: 0.5rem;
        }
        .add-user-btn__text {

        }
      }

      .search-box {
          margin: 1rem;
          position: relative;

          .search-icon {
            position: absolute;
            top: 50%;
            left: 0;
            transform: translateY(-50%);
          }

          .search-input {
            padding: 0.5rem 2rem;
          }
      }

      .filters {
          margin: 1rem;
          display: flex;
          justify-content: center;

          .filter-item {
              cursor: pointer;

              &:not(:last-child) {
                  margin-right: 3rem;
              }

              &.filter-item--active {
                font-weight: bold;
              }
          }
      }

      .user-list {
          margin: 1rem;

          .user-item {
              width: 100%;
              display: flex;
              align-items: center;
              cursor: pointer;


              &:not(:first-child) {
                  margin-top: 1rem;
              }

              .user-item__image {
                img {
                    border-radius: 90%;
                    width: 30px;
                    height: 30px;
                    border: 1px solid #e6e7e8;
                }
              }

              .user-item__name {
                  white-space: nowrap;
                  overflow: hidden;
                  text-overflow: ellipsis;
                  margin-left: 0.5rem;
              }
          }
      }
    }
</style>
