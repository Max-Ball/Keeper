import { AppState } from "../AppState";
import { api } from "./AxiosService";

class ProfilesService {

  async getProfile(id) {
    const res = await api.get(`api/profiles/${id}`)
    AppState.activeProfile = res.data
  }

  async getProfileVaults(id) {
    const res = await api.get(`/api/profiles/${id}/vaults`)
    AppState.vaults = res.data
  }

  async getProfileKeeps(id) {
    const res = await api.get(`/api/profiles/${id}/keeps`)
    AppState.keeps = res.data
  }
}

export const profilesService = new ProfilesService();