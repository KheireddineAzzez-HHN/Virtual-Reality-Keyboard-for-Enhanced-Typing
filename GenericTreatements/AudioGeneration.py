import random
import os
import string
from pydub import AudioSegment

def alter_click_sound(base_sound, pitch_variation=0.2, gain_variation=2):
    new_sample_rate = int(base_sound.frame_rate * (1 + random.uniform(-pitch_variation, pitch_variation)))
    altered_sound = base_sound._spawn(base_sound.raw_data, overrides={'frame_rate': new_sample_rate})
    altered_sound = altered_sound.set_frame_rate(base_sound.frame_rate)
    
    altered_sound = altered_sound + random.uniform(-gain_variation, gain_variation)
    
    return altered_sound

def main():
    click_sound = AudioSegment.from_file("path_to_click_sound_file.mp3")
    
    output_directory = "output_sounds"
    os.makedirs(output_directory, exist_ok=True)

    file_paths_varied = {}
    for letter in string.ascii_uppercase:
        varied_sound = alter_click_sound(click_sound)
        output_path = os.path.join(output_directory, f"{letter}_varied.mp3")
        varied_sound.export(output_path, format="mp3")
        file_paths_varied[letter] = output_path

    for letter, path in file_paths_varied.items():
        print(f"{letter}: {path}")

if __name__ == "__main__":
    main()
