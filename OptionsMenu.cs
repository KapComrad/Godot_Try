using Godot;
using System;

namespace UI
{
    public class OptionsMenu : Control
    {
        private Button _backButton;
        private Slider _volumeSlider;
        private Button _bassBoostCheckButton;
        public override void _Ready()
        {
            _volumeSlider = GetNode<Slider>("Panel/VBoxContainer/HBoxContainer/VolumeSlider");
            _bassBoostCheckButton = GetNode<Button>("Panel/VBoxContainer/HBoxContainer2/BassBoostCheckButton");
            _backButton = GetNode<Button>("Panel/BackButton");
        }

        private void BackButtonClicked()
        {
            if (_backButton.Pressed)
            {
                GetParent().GetNode<Control>("OptionsMenu").Visible = false;
                GetParent().GetNode<PanelContainer>("PauseContainer").Visible = true;
            }
        }

        private void VolumeSlider()
        {
            GD.Print("Changed");
            AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Master"),(float)_volumeSlider.Value);
        }
        
    }

}

