import {FormGroup} from "@angular/forms";

export function formToFormData(form: FormGroup) {
  const formData = new FormData();

  for (const key of Object.keys(form.value)) {
    const value = form.value[key];
    formData.append(key, value)
  }

  if (form.get('tags')?.value) {
    const tags = <string[]>form.get('tags')!.value;
    formData.delete('tags')
    tags.forEach(tag => formData.append('tags', tag));
  }

  const images = form.get('images')!.value;
  for (const image of images) {
    formData.append('images', image);
  }

  return formData;
}
