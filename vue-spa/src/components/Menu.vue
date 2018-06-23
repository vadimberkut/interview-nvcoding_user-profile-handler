<template>
  <div class="menu">
    <router-link to="/user/create" class="add-user-btn">
        <i class="fas fa-plus add-user-btn__icon"></i>
        <span class="add-user-btn__text">Add user</span>
    </router-link>
    <div class="search-box">
        <i class="fas fa-search search-icon"></i>
        <input v-on:keyup="searchTextChange" type="text" class="search-input">
    </div>
    <div class="filters">
        <div v-bind:class="{ 'filter-item--active': userProfilesRequestParams.enabled }" v-on:click="enabledClick(true, $event)" class="filter-item">Enabled</div>
        <div v-bind:class="{ 'filter-item--active': !userProfilesRequestParams.enabled }" v-on:click="enabledClick(false, $event)" class="filter-item">Disabled</div>
    </div>
    <div v-on:scroll="onUserListScroll" class="user-list">
        <router-link v-for="(item, index) in userProfiles" v-bind:key="`userProfile_${index}`" class="user-item" v-bind:to="'/user/edit/profile/' + item.id">
            <div class="user-item__image">
                <img v-if="item.imageUrlAbsolute" v-bind:src="item.imageUrlAbsolute" />
                <img v-else src="../assets/logo.png" />
            </div>
            <div class="user-item__name">{{ item.name }}</div>
        </router-link>
    </div>
  </div>
  
</template>

<script>
import _ from 'lodash';
import store from '../store.js';

export default {
  name: 'Menu',
  data () {
    return {
        userProfilesRequestParams: store.state.userProfilesRequestParams,
        userProfiles: store.state.userProfiles
    }
  },
  created: function() {
  },
  methods: {
      enabledClick: function(enabled, e) {
          store.state.userProfilesRequestParams.enabled = enabled;
          store.actions.filterUserProfiles(store.state.userProfilesRequestParams);
      },
      searchTextChange: _.debounce(function(e) {
          store.state.userProfilesRequestParams.searchText = e.target.value;
          store.actions.filterUserProfiles(store.state.userProfilesRequestParams);
      }, 500),
      onUserListScroll: function(e) {
          if(e.target.scrollTop + e.target.clientHeight >= e.target.scrollHeight) {
            let nextPage = store.state.userProfilesRequestParams.page + 1;
            if(nextPage >= store.state.userProfilesResponseParams.pages) {
                return;
            }
            let params = {
                ...store.state.userProfilesRequestParams,
                page: nextPage
            }
            store.actions.getUserProfiles(params);
          }
      }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style lang="scss" scoped>
    .menu {
      width: 25%;
      min-width: 250px;
      max-width: 300px;
      min-height: 100vh;
      max-height: 100vh;
      background-color: #f1f1f1;
      display: flex;
      flex-direction: column;

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
          overflow-y: scroll;

          .user-item {
              width: 100%;
              display: flex;
              align-items: center;
              cursor: pointer;

              a {
                display: flex;
                align-items: center;
              }

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
