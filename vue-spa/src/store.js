import _ from 'lodash';
import API from './utils/API';

let defaultUserProfilesRequestParams = {
    page: 0,
    pageSize: 10,
    searchText: null,
    enabled: true
};
let defaultUserProfile = {
    id: null,
    name: '',
    email: '',
    skypeLogin: '',
    signature: '',
    imageUrl: '',
    imageBase64: '', // for upload

    imageUrlAbsolute: '', // internal field
};

class Store {

    state = {
        userProfilesRequestParams: _.cloneDeep(defaultUserProfilesRequestParams),
        userProfilesResponseParams: {
            totalCount: 0,
            pages: 0,
        },
        userProfiles: [],
        roles: [],

        // User that is being created
        // creatableUserProfile: _.cloneDeep(defaultUserProfile),

        // User that is being edited
        editableUserProfile: _.cloneDeep(defaultUserProfile), // will be set on navigation to edit page
    }

    actions = {
        /** Loads new items and pushes them into existing array */
        getUserProfiles: (params) => {
            return API.userProfiles().getAll(params).then((response) => {
                let { requestParams, responseParams, data } = response;
                Object.assign(this.state.userProfilesRequestParams, requestParams);
                Object.assign(this.state.userProfilesResponseParams, responseParams);
                data.forEach(x => {
                    if(this.state.userProfiles.findIndex(y => y.id === x.id) === -1) {
                        this.state.userProfiles.push(x)
                    }
                });
                return data;
            }).catch((err) => {
                console.error(err);
                throw err;
            });
        },
        /** Loads filtred item list and resetes current items */
        filterUserProfiles: (params) => {
            return API.userProfiles().getAll(params).then((response) => {
                let { requestParams, responseParams, data } = response;
                Object.assign(this.state.userProfilesRequestParams, requestParams);
                Object.assign(this.state.userProfilesResponseParams, responseParams);
                this.state.userProfiles.splice(0, this.state.userProfiles.length);
                data.forEach(x => this.state.userProfiles.push(x));
                return data;
            }).catch((err) => {
                console.error(err);
                throw err;
            });
        },
        createUserProfile: (profile) => {
            delete profile.id;
            return API.userProfiles().create(profile).then((createdProfile) => {
                this.state.userProfiles.unshift(createdProfile);
                return createdProfile;
            }).catch((err) => {
                console.error(err);
                throw err;
            });
        },
        updateUserProfile: (profile) => {
            return API.userProfiles().update(profile).then((updatedProfile) => {
                this.actions.setEditableUserProfile(updatedProfile);
                let index = this.state.userProfiles.findIndex(x => x.id == updatedProfile.id);
                this.state.userProfiles.splice(index, 1, updatedProfile);

                return updatedProfile;
            }).catch((err) => {
                console.error(err);
                throw err;
            });
        },
        updateRole: (params) => {
            return API.userProfiles().updateRole(params).then((assignedRole) => {
                this.state.editableUserProfile.roleId = assignedRole.id;
                let index = this.state.userProfiles.findIndex(x => x.id == params.userProfileId);
                this.state.userProfiles.splice(index, 1, _.cloneDeep(this.state.editableUserProfile));

                return assignedRole;
            }).catch((err) => {
                console.error(err);
                throw err;
            });
        },
        updateSettings: (userProfileSettings) => {
            return API.userProfiles().updateSettings(userProfileSettings).then((updatedSettings) => {
                this.state.editableUserProfile.settings = updatedSettings;
                let index = this.state.userProfiles.findIndex(x => x.id == updatedSettings.userProfileId);
                this.state.userProfiles.splice(index, 1, _.cloneDeep(this.state.editableUserProfile));

                return updatedSettings;
            }).catch((err) => {
                console.error(err);
                throw err;
            });
        },

        /* Roles */
        getRoles: () => {
            return API.roles().getAll().then((roles) => {
                roles.forEach(x => {
                    if(this.state.roles.findIndex(y => y.id === x.id) === -1) {
                        this.state.roles.push(x)
                    }
                });
                return roles;
            }).catch((err) => {
                console.error(err);
                throw err;
            });
        },

        /* Other */
        resetEditableUserProfile: () => {
            // this.state.creatableUserProfile = _.cloneDeep(defaultUserProfile);
            Object.assign(this.state.editableUserProfile, defaultUserProfile);
        },
        setEditableUserProfile: (profile) => {
            this.actions.resetEditableUserProfile();
            Object.assign(this.state.editableUserProfile, profile);
        }
    }
};

let store = null;

export default (function(){
    if(store === null) {
        store = new Store();
        return store;
    }
})();