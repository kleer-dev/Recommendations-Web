import {FormGroup} from "@angular/forms";

export function formToFormData(form: FormGroup) {
  const formData = new FormData();

  for (const key of Object.keys(form.value)) {
    const value = form.value[key];
    formData.append(key, value)
  }

  const images = form.get('images')!.value;
  for (const image of images) {
    formData.append('images', image);
  }

  return formData;
}
